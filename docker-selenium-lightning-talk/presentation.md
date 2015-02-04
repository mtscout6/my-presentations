# Docker and Selenium

Selenium is used for browser automation testing

## Why is docker good for this?

Like common problems with a build infrastructure making sure your build agents
have all the necessary resources can be tedious and time consuming. Making sure
you have the right browser installed and maintaining a cluster of agents that
can execute tests in a browser can be tedious.

If a build agent gets out of wack rebuilding that machine can take time.

Docker to the rescue!

## Where do you get the images

https://registry.hub.docker.com/repos/selenium/

## How do I run them

- Hub: `docker run -d -p 4444:4444 --name hub selenium/hub:2.44.0`
- Chrome Node: `docker run -d --link hub:hub selenium/node-chrome:2.44.0`

Run `docker ps`

```
CONTAINER ID        IMAGE                         COMMAND                CREATED             STATUS              PORTS                     NAMES
6665923f1633        selenium/node-chrome:2.44.0   "/opt/bin/entry_poin   2 seconds ago       Up 1 seconds                                  goofy_pasteur
a7dc6f8b31d8        selenium/hub:2.44.0           "/opt/bin/entry_poin   7 seconds ago       Up 7 seconds        0.0.0.0:49155->4444/tcp   hub
```

Open the hub console in your browser
[http://dockerhost:4444](http://dockerhost:4444) to see that the selenium grid
is running and that you have an available node that can only run Chrome!

## Running tests

For this example I'll defer to an existing test in the
[docker-selenium](https://github.com/SeleniumHQ/docker-selenium/tree/2dae42a07ee1a4b586ade4e9b7b801101e9fc39e/Test) repo.

Run `docker run --rm -it --link hub:hub selenium/test:local`

## Console output isn't interesting though

How to use with VNC

- Chrome Debug Node: `docker run -d -p 5900:5900 --link hub:hub selenium/node-chrome-debug:2.44.0`
