import {Observable, Observer} from "rxjs";

//// To cut down on client code size...
// import {Observable} from "rxjs/Observable";
// import "rxjs/add/operator/map";
// import "rxjs/add/operator/filter";

let numbers = [1, 5, 10];
let source = Observable.create(observer => {
	let index = 0;

	let produceValue = () => {
		observer.next(numbers[index++]);

		if(index < numbers.length) {
			setTimeout(produceValue, 2000);
		} else {
			observer.complete();
		}
	}

	produceValue();
}).map(n => n * 2)
	.filter(n => n > 4);

// Easier way of subscribing to an observer,
// just pass in anon functions as next/error/complete
source.subscribe(
	value => console.log(`value: ${value}`),
	e => console.log(`error: ${e}`),
	() => console.log("complete")
);

// formal way of subscribing to an observer
class MyObserver implements Observer<number>{
	next(value) {
		console.log(`value: ${value}`);
	}

	error(e) {
		console.log(`error: ${e}`);
	}

	complete() {
		console.log("complete");
	}
}

// source.subscribe(MyObserver());