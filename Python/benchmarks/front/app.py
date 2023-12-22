from random import randint
from flask import Flask, request
import requests

PORT = 8040


def echo(size):
        payload = 'x' * size
        response = requests.get(f"http://localhost:{PORT}/echo", data=payload)
        res = response.text
        return res

def large_echo():
    return echo(4_000_000)

def small_echo():
    return echo(1)