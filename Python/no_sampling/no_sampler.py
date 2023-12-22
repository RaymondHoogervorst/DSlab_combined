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

def no_sample(func):
    def wrapper(*args, **kwargs):
        with tracer.start_as_current_span("tail_sample") as span:
            try:
                span.set_attribute("args", str(args))
                span.set_attribute("kwargs", str(args))
                res = func(*args, span=span, **kwargs)
            except Exception as e:
                span.set_attribute("exception", str(e))
                return "Error", 500
            else:
                return res
    return wrapper