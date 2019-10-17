export interface Question {
    TopicId: number;
    Question: string;
    QuestionId: number;
    Answer: {
        Id: number;
        Text: string;
    }[];
}