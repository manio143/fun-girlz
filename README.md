# VS Code with .NET Core 2.0 and F# (Mono)

This is a simple environment for working with .NET Core and interactively with F# in VS Code. You should be able to mount the X11 socket and use it with your dev folder.

Tested on Linux Mint 18.3

## Running
First add root permission for accessing X11

    xhost +SI:localuser:root

Run the container

    docker run \
        -e DISPLAY=unix$DISPLAY \
        -v $HOME/Code:/home/dev/workdir \
        -v /tmp/.X11-unix:/tmp/.X11-unix \
        --net=host \
        manio143/fscodeenv

If neccessary (though you may have a different issue) add

    -v $HOME/.Xauthority:/home/developer/.Xauthority

## Remove all containers with name containing '_' (generated names)

    docker ps -a | awk '{ print $1,$13,$14,$15 }' | grep '_' | awk '{ print $1 }' | xargs -I {} docker rm {}