# MetaWorldWeb
A library (and standard) for accessing, and interpreting existing web pages for use in world generators and gaming projects.

The library was born out of a simple PoC application to test the idea oof procedurally generated worlds by interpreting web pages.
At this stage the library is still a PoC and in development.

# Installation

You can use this library by cloning the git repository and linking to the project from your Visiual Studio project.

# Usage

There are two method for using the MetaWebManager class within a project.

You can call the AddMetaWorldWeb method within the start-up routine of a your web application, which will register the necessary classes with the Dependency Injection framework.
You ca then use the Manager class by inecting the IMetaWorldWeb interface to your classes.

Alternatively, if you're not using a DI framework, you can get an instance of the Manager class from a call to the ```MetaWorldWebFactory.CreateManager()``` method.

Once you have an instance of ```IMetaWroldWebManager```, you can call ```GetMetaWorldDataAsync``` to retrieve the web page meta world information.

More details will be added as the library is progressed.
If you're interested in the concept, try playing around with it and feel free to raise PRs for review.
