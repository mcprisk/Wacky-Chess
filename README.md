# Chess Networking Tests
Multiplayer Chess - but its inside a cube to be fancy.
## Initial Setup
To contribute to this project, create a new, empty unity project and initialize a local git repository within that folder:
```bash
$ git init
```
Then, set this repository as the remote origin:
```bash
$ git remote add origin https://github.com/UARTS-260-VR-3D-EFFECTS/Chess-Netorking-Tests.git"
```
Lastly, remove excess files from your local repository:
```bash
$ rm -rf Assets
$ rm -rf ProjectSettings
$ rm -rf Packages
```
## Making Changes
Next pull the remote repository into your local repository:
```bash
$ git pull origin master
```
Now you can make whatever changes you want in unity or an IDE of your choosing. You need to do this step every time you want to make a change!  

## Check for Remote Changes
After making your changes check to make sure your local repository is still up to date:
```bash
$ git fetch
$ git status
$ git pull origin master
```
If there were updates that were merged when you pulled, make sure your changes still work before continuing to the next step!

## Push your Changes
stage them to be commited by running:
```bash
$ git add "FilenameToAdd (use git status to see what files you've changed and only add ones you intended to change)"
```
Then commit your changes with a descriptive message:
```bash
$ git commit -m "This is (not) a very Descriptive Message!"
```
Finally push your changes to the repository:
```bash
$ git push origin master
```
**DO NOT USE "git push -f" OR "git push --force"**.
Ask for help if you are having trouble pushing to the repository first!
#
This project is a heavily modified version of GameDevChef/Chess ([found here](https://github.com/GameDevChef/Chess/tree/main)) - a 2D Chess game with abstraction so good it could be made into a cube.  Watch GameDevChef's tutorial on this project ([found here](https://youtu.be/cWgo0ak_8sE)) for an excellent understanding of how this project works.
