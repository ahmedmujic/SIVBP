import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { AcceptAnswerRequest, Post } from "../interfaces/post.interface";


@Injectable({
  providedIn: 'root',
})
export class PostService {
  private readonly userBaseUrl = `${environment.baseUrl}/posts`;
  constructor(private httpClient: HttpClient) {}

  public getPostDeatilsByPostId(id: number): Observable<Post> {
    return this.httpClient.get<Post>(`${this.userBaseUrl}/${id}`);
  }

  public acceptAnswer(request: AcceptAnswerRequest){
    return this.httpClient.post(`${this.userBaseUrl}/accept-answer`, request);
  }
}
