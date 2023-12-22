## Preqrequisites

The python servers are written in Flask. In order to run them you should first install flask:
> pip install 'flask<3' 'werkzeug<3'

To run opentelemetries, you also need to install the opentelemetry package:
> pip install opentelemetry-distro

> opentelemetry-bootstrap -a install


## Run front/middle/back server model without opentelemetry
Open 3 terminals, on each, open the virtual environment and cd into the bare directory:

> source venv/bin/activate

> cd benchmarks

### Terminal 1
This will be the front end. Execute the following:

> flask run -p 8000

### Terminal 2
This will be the middle end. Execute the following:

> cd middle

> flask run -p 8040

### Terminal 3
This will be the back end. Execute the following: 

> cd back

> flask run -p 8080

### Network benchmarks
Open the browser and go to either of these URL's:

> http://localhost:8000/small

> http://localhost:8000/large

### Compute benchmarks:

Open 1 terminal, on each, open the virtual environment and cd into the bare directory. :

> source venv/bin/activate
> cd benchmarks

Run the flask

> flask run -p 8000

In the browser, open the desired workload with one of these URL's:

> http://localhost:8000/pidigits

> http://localhost:8000/matrix

> http://localhost:8000/fac

# MANUAL: Running opentelemetry

## Run front/middle/back server model
Open 3 terminals, on each, open the virtual environment and cd into the bare directory:

> cd benchmarks

> export OTEL_PYTHON_LOGGING_AUTO_INSTRUMENTATION_ENABLED=true


### Terminal 1
This will be the front end. Execute the following:

> opentelemetry-instrument \
    --traces_exporter console \
    --metrics_exporter console \
    --logs_exporter console \
    --service_name dice-server \
    flask run -p 8000

### Terminal 2
This will be the middle end. Execute the following:

> cd middle

> opentelemetry-instrument \
    --traces_exporter console \
    --metrics_exporter console \
    --service_name dice-server \
    flask run -p 8040

### Terminal 3
This will be the back end. Execute the following: 

> cd back

> opentelemetry-instrument \
    --traces_exporter console \
    --metrics_exporter console \
    --service_name dice-server \
    flask run -p 8080

You should now be able to make requests similar as you did with the non-opentelemetry version, but now you should see the traces appear in the console.