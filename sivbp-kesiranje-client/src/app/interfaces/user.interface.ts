export interface User{
    displayName: string;
    aboutMe: string;
    downVotes: number;
    upVotes: number;
    views: number;
    commentedPosts: number[];
}

export interface UserEfficiency{
    id: number;
    displayName: string;
    location: string;
    numAnswers: number;
    numAccepted: number;
    acceptedPercent: number;
}