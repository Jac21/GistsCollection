import { Course } from '../courses/course';
import { IAppState } from './IAppState';
import { FILTER_COURSES } from './actions';

const courses = [
    {
        "id": 1,
        "name": "Building Apps with React",
        "topic": "ReactJS"
    },
    {
        "id": 2,
        "name": "Building Apps with Vue",
        "topic": "VueJS"
    },
    {
        "id": 3,
        "name": "Building Apps with Glimmer",
        "topic": "Glimmer"
    },
];

const initialState: IAppState = {
    courses,
    filteredCourses: courses
};

function filterCourses(state, action) : IAppState {
    return Object.assign({}, state, {
        filteredCourses: state.courses.filter(c => c.name.toLowerCase().indexOf
        (action.searchText.toLowerCase()))
    })
}

export function reducer(state= initalState, action) {
    switch(action.type) {
        case FILTER_COURSES:
            return filterCourses(state, action);
        default:
            return state;
    }
}