# C Sharp Scanner Class

This is a small side project, I have recreated the Scanner object found in Java
in C#. There were some restriction due to the nature of the .net framework.
However I have tried my best to faithfully recreate the majority of functions.

The original code I was working from can be found
[here](https://github.com/openjdk-mirror/jdk7u-jdk/blob/master/src/share/classes/java/util/Scanner.java)

The original Scanner java class uses a regex method to detect the item that
needs to be returned on a next* call. However, this is done by reading from a
Readable object handed to it upon its construction. Unfortunately, there is no
such object type in the .net framework. This it probably due to the java virtual
machine.

Having now complete the more basic functionality of the class I have a rough
Idea of how I would like to proceed with the project.

I would like to create multiple scanner Classes each targeted at looking for a
certain type. For example there would be a StringScanner a NumberScanner e.c.t.
each can be customized to then further refile what they look for. Each scanner
could then be given further customisation with in built ENUMS that can be past
to the constructor.