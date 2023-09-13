# Prerequisites

➜  maelstrom git:(master) ✗ brew install openjdk graphviz gnuplot

➜  maelstrom git:(master) ✗ sudo ln -sfn /opt/homebrew/opt/openjdk/libexec/openjdk.jdk /Library/Java/JavaVirtualMachines/openjdk.jdk

➜  maelstrom git:(master) ✗ echo 'export PATH="/opt/homebrew/opt/openjdk/bin:$PATH"' >> ~/.zshrc

➜  maelstrom git:(master) ✗ export CPPFLAGS="-I/opt/homebrew/opt/openjdk/include"               
