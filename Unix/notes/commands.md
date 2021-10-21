# Display the Contents of a File: cat

> cat sos.conf

> cat sos.conf sos-two.conf

> cat sos.conf sos-two.conf > new-conf.conf

# Associate Actions to File Types: mimeopen

> mimeopen -d kernel-article.mm

# Set File Attributes: chmod

0: No permission
1: Execute permission
2: Write permission
3: Write and execute permissions
4: Read permission
5: Read and execute permissions
6: Read and write permissions
7: Read, write and execute permissions

> ls -l notes.txt
> chmod 764 notes.txt
> ls -l notes.txt

# Find a String: grep

> grep jeremy /etc/passwd

> ps -e | grep naut

# Find File Differences: diff

> diff core.c old-core.c

> diff -y -W 70 core.c old-core.c