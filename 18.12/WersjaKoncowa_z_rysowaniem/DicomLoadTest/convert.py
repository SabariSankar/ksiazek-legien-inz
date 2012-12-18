import os;
from subprocess import call;
import re;
import shutil;

#wymaga dcmdjpg

files=os.listdir(".");
files.remove("b.py");

for f in files:
    if 'y' in f:
        os.remove(f);

try:
    os.makedirs("tmp");
except Exception:
    pass;

files=os.listdir("C:\Users\Grzegorz\Downloads\Chest\JW");
for f in files:
    print f;
    call(
        "dcmdjpeg.exe {0} tmp/{0}y.dcm".format(f));

