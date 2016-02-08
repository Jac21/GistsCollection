import {setEntries, next, vote} from './core';

export default function reducer(state, action) {
	// figure out which fuction to call and call it
	switch(action.type) {
		case 'SET_ENTRIES':
			return setEntries(state, action.entries);
		case 'NEXT':
			return next(state);
		case 'VOTE':
			return vote(state, action.entry)
	}
	return state;
}