# Match3-NUnit-Test
Using NUnity.Framework for Match3 tutorial methods

## Why Write Unit Tests
Unit Testing can reduce the defect rate.
Plus, the sooner you catch the bug, the cheaper it is.
If it got out to production, it already may have caused bad user experience, plus it may be hard to pin down based on just user helpdesk tickets.
If the bug was noticed in the development branch, it is not obvious which exact feature caused it, and where to look.
But if you just made the change and see the test broke, you still probably hold a lot of relative info or may even know a few lines of code you just changed that lead to it.

When adding new functionality it can be really handy to be able to test it without leaving the IDE. And because you probably somehow test your new code anyway, this will leave those test cases for later, when anyone comes to change this code, and avoid regression.

If the code already has unit-tests that check if the class behaves as expected, you can change the internal implementation with immediate feedback.

They improve the debugging cycle. If you have a reproduction case in code that you can rerun in less than a second after you add logs or introduce changes, instead of waiting for the game to load or even going into a particular screen to reproduce it – you can make a lot more iterations in the same amount of time.

## How to write Unit Tests in Unity []
