using UnityEngine;
using System.Collections;

public class ProceduralTerrain : MonoBehaviour {
		
	float[] surfFA;

	
	void Start () {
		int size = 11;
		int randomSeed = Random.Range(-100, 100);
		float HEIGHT_SCALE = 1.0f;
		float surfaceH = 0.7f;

		surfFA = alloc2DFractArray(size);	
		if(surfFA == null)
			return;
		fill2DFractArray(surfFA, size, randomSeed, HEIGHT_SCALE, surfaceH); 
	}

	// avgEndpoints - Given the i location and a stride to the data
	// values, return the average those data values. "i" can be thought of
	// as the data value in the center of two line endpoints. We use
	// "stride" to get the values of the endpoints. Averaging them yields
	// the midpoint of the line.
	//
	// Called by fill1DFractArray.
	float avgEndpoints (int i, int stride, float[] fa) {
		return ((float) (fa[i-stride] + fa[i+stride]) * .5f);
	}
	
 	// avgDiamondVals - Given the i,j location as the center of a diamond,
	// average the data values at the four corners of the diamond and
	// return it. "Stride" represents the distance from the diamond center
	// to a diamond corner.
	//
	// Called by fill2DFractArray.	
	float avgDiamondVals (int i, int j, int stride,
		                             int size, int subSize, float[] fa) {
		// In this diagram, our input stride is 1, the i,j location is
        // indicated by "X", and the four value we want to average are
        // "*"s:
        //   .   *   .
		//
        //   *   X   *
		//
        //   .   *   .
		
		// In order to support tiled surfaces which meet seamless at the
		// edges (that is, they "wrap"), We need to be careful how we
		// calculate averages when the i,j diamond center lies on an edge
		// of the array. The first four 'if' clauses handle these
		// cases. The final 'else' clause handles the general case (in
		// which i,j is not on an edge).

		if (i == 0)
			return ((float) (fa[(i*size) + j-stride] +
			                 fa[(i*size) + j+stride] +
			                 fa[((subSize-stride)*size) + j] +
			                 fa[((i+stride)*size) + j]) * .25f);
		else if (i == size-1)
			return ((float) (fa[(i*size) + j-stride] +
			                 fa[(i*size) + j+stride] +
			                 fa[((i-stride)*size) + j] +
			                 fa[((0+stride)*size) + j]) * .25f);
		else if (j == 0)
			return ((float) (fa[((i-stride)*size) + j] +
			                 fa[((i+stride)*size) + j] +
			                 fa[(i*size) + j+stride] +
			                 fa[(i*size) + subSize-stride]) * .25f);
		else if (j == size-1)
			return ((float) (fa[((i-stride)*size) + j] +
			                 fa[((i+stride)*size) + j] +
			                 fa[(i*size) + j-stride] +
			                 fa[(i*size) + 0+stride]) * .25f);
		else
			return ((float) (fa[((i-stride)*size) + j] +
			                 fa[((i+stride)*size) + j] +
			                 fa[(i*size) + j-stride] +
			                 fa[(i*size) + j+stride]) * .25f);
	}
	
 	// avgSquareVals - Given the i,j location as the center of a square,
 	// average the data values at the four corners of the square and return
 	// it. "Stride" represents half the length of one side of the square.
 	//
 	// Called by fill2DFractArray.
	float avgSquareVals (int i, int j, int stride, int size, float[] fa) {
		// In this diagram, our input stride is 1, the i,j location is
    	// indicated by "*", and the four value we want to average are
       	// "X"s:
        //   X   .   X
		//
        //   .   *   .
		//
        //   X   .   X
		return ((float) (fa[((i-stride)*size) + j-stride] +
		                 fa[((i-stride)*size) + j+stride] +
		                 fa[((i+stride)*size) + j-stride] +
		                 fa[((i+stride)*size) + j+stride]) * .25f);
	}

	// fill1DFractArray - Tessalate an array of values into an
 	// approximation of fractal Brownian motion.
	void fill1DFractArray (float[] fa, int size,
	                       int seedValue, float heightScale, float h)
	{
		int	i;
		int	stride;
		int subSize;
		float ratio, scale;
		
		if (!(size%2 == 1) || (size==1)) {
			// We can't tesselate the array if it is not a power of 2.
			print ("Error: fill1DFractArray: size %d is not a power of 2.\n");
			return;
		}
		
		// subSize is the dimension of the array in terms of connected line
       	// segments, while size is the dimension in terms of number of
       	// vertices.
		subSize = size;
		size++;
		
//		// initialize random number generator
//		srandom (seedValue);
		
//		#ifdef DEBUG
//		printf ("initialized\n");
//		dump1DFractArray (fa, size);
//		#endif
		
		// Set up our roughness constants.
	   	// Random numbers are always generated in the range 0.0 to 1.0.
	   	// 'scale' is multiplied by the randum number.
	   	// 'ratio' is multiplied by 'scale' after each iteration
	   	// to effectively reduce the randum number range.
		ratio = (float) Mathf.Pow(2.0f,-h);
		scale = heightScale * ratio;
		
		// Seed the endpoints of the array. To enable seamless wrapping,
       	// the endpoints need to be the same point. */
		stride = subSize / 2;
		fa[0] =
			fa[subSize] = 0.0f;
		
//		#ifdef DEBUG
//		printf ("seeded\n");
//		dump1DFractArray (fa, size);
//		#endif
		
		while (stride != 0) {
			for (i=stride; i<subSize; i+=stride) {
				fa[i] = scale * Random.Range (-0.5f, 0.5f) +
					avgEndpoints (i, stride, fa);
				
				// reduce random number range
				scale *= ratio;
				
				i+=stride;
			}
			stride >>= 1;
		}
		
//		#ifdef DEBUG
//		printf ("complete\n");
//		dump1DFractArray (fa, size);
//		#endif
	}
	
 	// fill2DFractArray - Use the diamond-square algorithm to tessalate a
 	// grid of float values into a fractal height map.
	void fill2DFractArray (float[] fa, int size,
	                       int seedValue, float heightScale, float h)
	{
		int	i, j;
		int	stride;
		int	oddline;
		int subSize;
		float ratio, scale;
		
		if (!(size % 2 == 1) || (size==1)) {
			// We can't tesselate the array if it is not a power of 2.

			print ("Error: fill2DFractArray: size " + size + " is not a power of 2.");
			return;
		}
		
		// subSize is the dimension of the array in terms of connected line
       	// segments, while size is the dimension in terms of number of
       	// vertices. */
		subSize = size;
		size++;
		
//		// initialize random number generator
//		srandom (seedValue);
		
//		#ifdef DEBUG
//		printf ("initialized\n");
//		dump2DFractArray (fa, size);
//		#endif
		
		// Set up our roughness constants.
	   	// Random numbers are always generated in the range 0.0 to 1.0.
	   	// 'scale' is multiplied by the randum number.
	   	// 'ratio' is multiplied by 'scale' after each iteration
	   	// to effectively reduce the randum number range.
		ratio = (float) Mathf.Pow (2.0f,-h);
		scale = heightScale * ratio;
		
		// Seed the first four values. For example, in a 4x4 array, we
       	// would initialize the data points indicated by '*':
		//
        //   *   .   .   .   *
		//
        //   .   .   .   .   .
		//
        //   .   .   .   .   .
		//
        //   .   .   .   .   .
		//
        //   *   .   .   .   *
		//
       	// In terms of the "diamond-square" algorithm, this gives us
       	// "squares".

       	// We want the four corners of the array to have the same
       	// point. This will allow us to tile the arrays next to each other
       	// such that they join seemlessly.
		
		stride = subSize / 2;
		fa[(0*size)+0] =
			fa[(subSize*size)+0] =
				fa[(subSize*size)+subSize] =
				fa[(0*size)+subSize] = 0.0f;
		
//		#ifdef DEBUG
//		printf ("seeded\n");
//		dump2DFractArray (fa, size);
//		#endif
		
		// Now we add ever-increasing detail based on the "diamond" seeded
       	// values. We loop over stride, which gets cut in half at the
       	// bottom of the loop. Since it's an int, eventually division by 2
       	// will produce a zero result, terminating the loop.
		while (stride != 0) {
			// Take the existing "square" data and produce "diamond"
		   	// data. On the first pass through with a 4x4 matrix, the
		   	// existing data is shown as "X"s, and we need to generate the
	       	// "*" now:
			//
            //   X   .   .   .   X
			//
            //   .   .   .   .   .
			//
            //   .   .   *   .   .
			//
            //   .   .   .   .   .
			//
            //   X   .   .   .   X
			//
			// It doesn't look like diamonds. What it actually is, for the
			// first pass, is the corners of four diamonds meeting at the
	      	// center of the array.
			for (i=stride; i<subSize; i+=stride) {
				for (j=stride; j<subSize; j+=stride) {
					fa[(i * size) + j] =
						scale * Random.Range(-0.5f, 0.5f) +
							avgSquareVals (i, j, stride, size, fa);
					j += stride;
				}
				i += stride;
			}
//			#ifdef DEBUG
//			printf ("Diamonds:\n");
//			dump2DFractArray (fa, size);
//			#endif
			
			// Take the existing "diamond" data and make it into
	       	// "squares". Back to our 4X4 example: The first time we
	       	// encounter this code, the existing values are represented by
	       	// "X"s, and the values we want to generate here are "*"s:
			//
            //   X   .   *   .   X
			//
            //   .   .   .   .   .
			//
            //   *   .   X   .   *
			//
            //   .   .   .   .   .
			//
            //   X   .   *   .   X
			//
	       	// i and j represent our (x,y) position in the array. The
	       	// first value we want to generate is at (i=2,j=0), and we use
	       	// "oddline" and "stride" to increment j to the desired value.
			oddline = 0;
			for (i=0; i<subSize; i+=stride) {
				//oddline = (oddline == 0);
				if(oddline == 0) oddline = 1;
				else oddline = 0;
				for (j=0; j<subSize; j+=stride) {
					if ((oddline != 0) && j == 0) j+=stride;
					
					// i and j are setup. Call avgDiamondVals with the
				   	// current position. It will return the average of the
				   	// surrounding diamond data points.
					fa[(i * size) + j] =
						scale * Random.Range(-0.5f, 0.5f) +
							avgDiamondVals (i, j, stride, size, subSize, fa);
					
					// To wrap edges seamlessly, copy edge values around
				   	// to other side of array
					if (i==0)
						fa[(subSize*size) + j] =
							fa[(i * size) + j];
					if (j==0)
						fa[(i*size) + subSize] =
							fa[(i * size) + j];
					
					j+=stride;
				}
			}
//			#ifdef DEBUG
//			printf ("Squares:\n");
//			dump2DFractArray (fa, size);
//			#endif
			
			// reduce random number range.
			scale *= ratio;
			stride >>= 1;
		}
		
//		#ifdef DEBUG
//		printf ("complete\n");
//		dump2DFractArray (fa, size);
//		#endif
	}

	float[] alloc2DFractArray (int size)
	{
		// For a sizeXsize array, we need (size+1)X(size+1) space. For
       	// example, a 2x2 mesh needs 3x3=9 data points: 
		//
        //   *   *   *
		//
        //   *   *   *
		//
        //   *   *   *

       	// To account for this, increment 'size'.
		size++;

		return new float[size * size]; 
		//return ((float *) malloc (sizeof(float) * size * size));
	}

	void Update () {
	
	}
}
