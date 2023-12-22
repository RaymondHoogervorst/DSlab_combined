from random import randint
# Matrix multiplication

def matrixmul():
    N = 50

    matrixA = [[randint(0, 100) for i in range(N)] for j in range(N)]
    matrixB = [[randint(0, 100) for i in range(N)] for j in range(N)]

    result = [[sum(matrixA[i][k] * matrixB[k][j] for k in range(N)) for i in range(N)] for j in range(N)]

    return result