import { Course } from '../courses/course';
import { IAppState } from './IAppState';

const initalState: IAppState = {
    courses: [
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
    ]
};

export function reducer(state= initalState, action) {
    return state;
}