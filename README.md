# Pricing Engine 

The goal of the task is to create an application that will replace an Excel file where financial calculations are performed.

Excel file: [https://docs.google.com/spreadsheets/d/14ADRGpLEVdvoWGG6APVOdBnNEU7dxqgT8RPM1gIfaXI/](https://docs.google.com/spreadsheets/d/14ADRGpLEVdvoWGG6APVOdBnNEU7dxqgT8RPM1gIfaXI/edit?usp=sharing)

You have three types of input in the file.

User inputs - such inputs that you will receive from the user are grouped here

Database inputs - You should be able to retrieve the data listed here from the database

Calculated inputs - you have to calculate the data listed here yourself

After the Inputs, there are columns that contain the formulas to be calculated. Some columns depend on the value of another column. Your goal is to move these calculations into the application so that changes can be easily made if other calculations are added in the future.

The application must have unit tests.

After the calculation, the API should return the sum of the Ending balance column values, excluding the first value (see box T15).