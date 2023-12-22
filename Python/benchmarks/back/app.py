from random import randint
from flask import Flask, request
import json

app = Flask(__name__)

@app.route("/echo")
def echo():
    data = request.data
    return "Recieved", 200