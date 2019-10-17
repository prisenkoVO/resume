export interface News{
  Photo: string;
  UserPhoto: string;
  Title: string;
  Text: string;
  isLiked?: boolean;
  Likes?: number;
  Id?: number;
  comments?: object[];
  Author?: string;
  Date?: string;
  
}
