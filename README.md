# C Sharp Scanner Class

## What is it?

This class library contains some basic Scanner utilities that can be used as a
quick solution to needing to process text information. This can be extremely
useful in trying out or modelling a new project and you do not want to mess around with the
inbuilt regex class. The main advantage on this is you can "hot swap" what you
want returned (More detail given in the **Alternating Different Next Calls** section).

Currently there is only a StringScanner class which requires you to pass it a
string in the constructor stage. This class can only return the next line,
double or integer (32-bit).

## Why build this?

The project was originally inspired by the Java Scanner class.

I was learning C# by re-implementing some code I had in Java and I was
frustrated by the lack of a general purpose Scanner class, so I decided to
create my own.

The original code I was working from can be found
[here](https://github.com/openjdk-mirror/jdk7u-jdk/blob/master/src/share/classes/java/util/Scanner.java)

Unfortunately, I can see a way to truly recreate a single multi use scanner
class so I have separated the monolithic Scanner class into smaller Single use
classes e.g. the StringScanner class. However, I have kept to the general
principles used in the original that is to use regex to scan the text.

## Unique Features

### Alternating Different Next Calls (StringScanner)

Once initialized the StringScanner you can alternate the different Next* calls.
For example if we gave it the string 

```
"this is a test \n 94 rest of the line\n
```

the following line" these calls would return the following.

```
NextLine() => "this is a test "
NextInt() => 94
NextLine() => " rest of the line"
NextLine() =>the following line"
```

## Future work

Currently there is only one scanner. However, I intend to add some more basic
ones such as a ConsoleScanner and a FileScanner. I also intend to add more
robust regexes and next calls to each Scanner to allow for wider use e.g.
NextDate, NextTime e.c.t. This should make them much more general purpose.