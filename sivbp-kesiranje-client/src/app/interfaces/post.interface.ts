export interface Comment{
    text: string;
    userId: number;
    userDisplayName: string;
    acceptedAnswerId: number;
}

export interface Post{
    postText: string;
    postTitle: string;
    acceptedAnswerId: number;
    comments: Comment[]
}

export interface AcceptAnswerRequest{
    commentId: number;
    postId: number;
}