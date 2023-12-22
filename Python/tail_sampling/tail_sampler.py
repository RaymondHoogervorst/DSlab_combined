from opentelemetry import trace
from opentelemetry.sdk.trace import TracerProvider
from opentelemetry.sdk.trace.export import (
    BatchSpanProcessor,
    ConsoleSpanExporter,
)

provider = TracerProvider()
processor = BatchSpanProcessor(ConsoleSpanExporter())
provider.add_span_processor(processor)
trace.set_tracer_provider(provider)
tracer = trace.get_tracer(__name__)

class TailSampleSpan(object):
    def __init__(self):
        self.attributes = {}

    def set_attribute(self, key, value):
        self.attributes[key] = value


def tail_sample(func):
    def wrapper(*args, **kwargs):
        psuedoSpan = TailSampleSpan()
        try:
            res = func(*args, span=psuedoSpan, **kwargs)
        except Exception as e:
            with tracer.start_as_current_span("tail_sample") as span:
                span.set_attribute("exception", e.message)
                span.set_attribute("args", str(args))
                span.set_attribute("kwargs", str(args))
                for key, value in psuedoSpan.attributes.items():
                    span.set_attribute(key, str(value))
            return "Error", 500
        else:
            return res
    return wrapper