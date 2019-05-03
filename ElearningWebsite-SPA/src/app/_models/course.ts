import { Video } from './video';

export interface Course {
    courseId: number;
    name: string;
    coverUrl: string;
    avaUrl: string;
    requirement: string;
    description: string;
    createdDate: Date;
    video: Video[];
}
