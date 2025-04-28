import os
import sys

# Get the directory containing this script
script_dir = os.path.dirname(os.path.abspath(__file__))

# Import and run translate.py with the original arguments
sys.path.insert(0, script_dir)
import translate

if __name__ == "__main__":
    translate.main()