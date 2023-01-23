# ESG-task
Upon launching, navigate to https://localhost:7213/
- You will see a chart with the two given datasets plotted. Pressure on the left Y axis, moment magnitude on the right y axis with time as the x.
- There are also two buttons - the first one 'Calculate regression' will calculate the linear regression for the event data using C#'s DateTime.MinValue as x=0, and the number of seconds since then as the X values for the calculation.
- The second button 'Calculaate regression (shifted)' will perform the same calculation, but this time with the first time in the dataset as x=0, and the number of seconds since then as the x values.

![image](https://user-images.githubusercontent.com/22601246/214085257-8387dc92-dbee-4e90-9d27-c0e57432e49d.png)
