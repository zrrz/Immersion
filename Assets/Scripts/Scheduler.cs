using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scheduler : MonoBehaviour {

	Queue<Task> tasks;

	static Scheduler s_instance;

	public delegate void TaskDelegate();

	Dictionary<TaskDelegate, float> taskTimes;

	const float MAX_FRAME_TIME = 1f/120f;

	public bool run = false;

	class Task {
		public Task(TaskDelegate p_myDelegate, bool p_repeating) {
			myDelegate = p_myDelegate;
			repeating = p_repeating;
		}
		public Task() {}
		public TaskDelegate myDelegate;
		public bool repeating;
	}

	public static void AddTask(TaskDelegate task, bool repeating) {
		instance.tasks.Enqueue(new Task(task, repeating));
	}
	
	void Start () {
		s_instance = this;
		tasks = new Queue<Task>();
		taskTimes = new Dictionary<TaskDelegate, float>();

		tasks.Enqueue(new Task(Dance, true));
	}
	
	void Update () {
		if(!run)
			return;
		if(tasks.Count > 0) {
			int tasksDone = 0;						//Debug
			Task task = tasks.Dequeue();
			if(taskTimes.ContainsKey(task.myDelegate)) {
				float curTasktime = taskTimes[task.myDelegate];
				DoTask(task);
				tasksDone++;						//Debug

				bool moreTasks = true;
				while(moreTasks) {
					if(tasks.Count == 0) {
						moreTasks = false;
						break;
					}
					if(taskTimes.ContainsKey(tasks.Peek().myDelegate)) {
						if(curTasktime + taskTimes[tasks.Peek().myDelegate] < MAX_FRAME_TIME) {
							task = tasks.Dequeue();
							DoTask(task);
							tasksDone++;			//Debug
							curTasktime += taskTimes[task.myDelegate];
							if(curTasktime > MAX_FRAME_TIME) {
								moreTasks = false;
							}
						} else {
							moreTasks = false;
						}
					}
				}
			} else {
				float curTime = Time.realtimeSinceStartup;
				DoTask(task);
				tasksDone++;						//Debug
				taskTimes.Add(task.myDelegate, Time.realtimeSinceStartup - curTime);
				print ("Task " + task.myDelegate.ToString() + " takes " + (Time.realtimeSinceStartup - curTime));
			}
			print ("Tasks done: " + tasksDone);		//Debug
		}
	}

	void Dance() {
		for(int i = 0; i < 1000; i++) {
			for(int j = 0; j < 100; j++) {
			
			}
		}
	}

	void DoTask(Task task) {
		task.myDelegate();
		if(task.repeating)
			tasks.Enqueue(task);
	}
	
	public static Scheduler instance {
		get {
			return s_instance;
		}
	}
}