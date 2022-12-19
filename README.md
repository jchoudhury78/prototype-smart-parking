# hackathon-submission-template
Please use this template for submitting solutions for the Microsoft Future Technology Hackathon 2022

In urban cities. finding a parking space in public places is becoming very challenging these days. The porotype that we have created is to address this issue. The solution provided assumes that state change sensors will be available in parking spots that will send the location information to IoT central whenever the parking spot status changes from occupied to vacant and vice versa. From IoT central, the message is exported to Azure Service Bus that triggers Azure function that inserts the data in persistent data store. Users can view the vacant parking spots on Azure maps in the web application screen by searching for the location where they want to park their vehicles. 

![image](https://user-images.githubusercontent.com/32918137/208426453-94deea07-9909-4a74-85f1-c817c4b8cee2.png)

The ParkingAvailabilityViewer Solution consists of two projects. 
1. Function App "MonitorParkingSpotFunctionApp" that fetches json data sent by parking sensors from Service Bus and puts in Azure SQL database for consumption by the Viewer app that shows the available parking spots.
2. Azure Web App "SmartParking" that displays the avaialble parking spots in Azure Map based on the location that the user wants to search.
