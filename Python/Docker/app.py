from flask import Flask
app = Flask('Example Flask Webserver (in Docker!')

@app.route('/')
def get_data():
    return "0"