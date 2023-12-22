from flask import Flask

from pidigits.pidigits import main as pidigits
from front.app import small_echo, large_echo
from matrixmul.app import matrixmul
from factorial.app import factorial

app = Flask(__name__)
app.add_url_rule("/pi", "pidigits", pidigits)
app.add_url_rule("/matrix", "matrix", matrixmul)
app.add_url_rule("/fac", "factorial", factorial)
app.add_url_rule("/small", "small_echo", small_echo)
app.add_url_rule("/large", "large_echo", large_echo)