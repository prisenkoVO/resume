export interface Message{
    text: string,
    isClientMessage: boolean, // Check is it user or bot message
    isLastMessage: boolean,
}