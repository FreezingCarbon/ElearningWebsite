import { Video } from './video';

export class Course {
    id: number;
    name: string;
    coverUrl: string;
    avaUrl: string;
    video: Video[];
}
