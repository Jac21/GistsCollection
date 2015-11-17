"""

Open - 

Bad example:
f = open('photo.jpg', 'r+')
jpgdata = f.read()
f.close()

If there is an error, close will not occur, better method:

with open('photo.jpg', 'r+') as f:
    jpgdata = f.read()


Mode arguments:

If you want to read the file, pass in r
If you want to read and write the file, pass in r+
If you want to overwrite the file, pass in w
If you want to append to the file, pass in a

"""

import io

with open('photo.jpg', 'rb') as inf:
	jpgdata = inf.read()

if jpgdata.startswith(b'\xff\xd8'):
    text = u'This is a JPEG file (%d bytes long)\n'
else:
    text = u'This is a random file (%d bytes long)\n'

with io.open('summary.txt', 'w', encoding='utf-8') as outf:
	outf.write(text % len(jpgdata))