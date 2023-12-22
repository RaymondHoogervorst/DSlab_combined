from flask import Flask
import random

from no_sampler import no_sample

app = Flask(__name__)

@app.route("/")
@no_sample
def fac(n = 100, span = None):
    p = 1
    for i in range(1, n+1):
        p *= i

    span.set_attribute("res", p)

    # Randomly crash 10% of the time
    if random.random() < 0.1:
        raise Exception()

    return str(p)