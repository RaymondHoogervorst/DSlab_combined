from random import randint
from flask import Flask, request

def extract_Digit(nth):
    global tmp1, tmp2, acc, den, num
    tmp1 = num * nth
    tmp2 = tmp1 + acc
    tmp1 = tmp2 // den

    return tmp1

def eliminate_Digit(d):
    global acc, den, num
    acc = acc - den * d
    acc = acc * 10
    num = num * 10

def next_Term(k):
    global acc, den, num
    k2=k*2+1
    acc = acc + num * 2
    acc = acc * k2
    den = den * k2
    num = num * k

def main():
    global tmp1, tmp2, acc, den, num
    n=100

    tmp1 = 0
    tmp2 = 0

    acc = 0
    den = 1
    num = 1


    i=0
    k=0
    res = ""
    while i<n:
        k+=1
        next_Term(k)

        if num > acc:
            continue


        d=extract_Digit(3)
        if d!=extract_Digit(4):
            continue

        res += chr(48+d)
        i+=1
        if i%10==0:
            res += "\t:%d" % (i) + "\n"
        eliminate_Digit(d)

    return res

app = Flask(__name__)