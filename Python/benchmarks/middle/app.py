from flask import Flask, request
import requests

PORT = 8080

app = Flask(__name__)

@app.route("/echo")
def echo():
        response = requests.get(f"http://localhost:{PORT}/echo", data=request.data)

        # Response
        res = response.text
        return res