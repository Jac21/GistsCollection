import { reducer } from './reducer';
import { FILTER_COURSES } from '../courses/course.actions';

describe('Reducer', () => {
    it('Should have the correct initial state', () => {
        const state = reducer(undefined, {});

        expect(state.courses.length).toBe(0);
        expect(state.filteredCourses.length).toBe(0);
    })
})