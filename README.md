# ZetaScheduler

ZetaScheduler is a scheduling library designed to efficiently manage and execute scheduled tasks within .NET applications. It provides a flexible and reliable solution for handling recurring or one-time tasks with precision.

## Features

- **Flexible Scheduling:** Supports various scheduling intervals, including daily, weekly, monthly, or custom-defined periods.
- **Task Management:** Allows adding, removing, and updating scheduled tasks dynamically at runtime.
- **Concurrency Handling:** Ensures thread-safe execution of tasks, preventing race conditions and ensuring data integrity.
- **Extensibility:** Easily extendable to accommodate custom scheduling needs or integrate with other systems.

## Installation

To include ZetaScheduler in your project, add the project reference to your .NET solution:

```bash
dotnet add package ZetaScheduler
```

## Usage

Here's a basic example of how to use ZetaScheduler:

1. Initialize the scheduler:

   ```csharp
   var scheduler = new ZetaScheduler.Core.ZetaScheduler();
   ```

2. Define a task to be scheduled:

   ```csharp
   public class CurrentTimeJob : IJob {
        public void Execute() {
            Console.WriteLine(DateTime.Now);
        }
   }
   ```

3. Schedule the task:

   ```csharp
   scheduler.ScheduleTask(new CurrentTimeJob(), DateTime.Now.AddMinutes(1));
   ```

    In this example, the task will be scheduled to execute in one minute.

4. To also make the task repeat on a specific interval:

    ```csharp
    scheduler.ScheduleTaskRecurring(new CurrentTimeJob(), DateTime.Now.AddMinutes(1), TimeSpan.FromMinutes(1));
    ```

      Now the task will be scheduled in one minute to be executed every minute.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes. Ensure that your code adheres to the project's coding standards and includes appropriate tests.

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/Dievaid/ZetaScheduler/blob/master/LICENSE.txt) file for more details.
