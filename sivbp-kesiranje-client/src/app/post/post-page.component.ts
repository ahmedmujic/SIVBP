import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { Post } from "../interfaces/post.interface";
import { PostService } from "../services/post.service";


@Component({
  selector: 'post-page',
  templateUrl: './post-page.component.html',
  styleUrls: ['./post-page.component.css']
})
export class PostPageComponent implements OnInit, OnDestroy {
  postId: number;
  post: Post;

  private readonly destroySub$ = new Subject();
  constructor(private postService: PostService,
    private route: ActivatedRoute) { }

  ngOnInit() {
     this.route.params.pipe(takeUntil(this.destroySub$)).subscribe(params => {
      this.postId = params['id'];
      this.getPostDetailsById();
    });
  }

  getPostDetailsById(){
    this.postService.getPostDeatilsByPostId(this.postId).pipe(takeUntil(this.destroySub$)).subscribe((data: Post) => {
      this.post = data;
    })
  }

  acceptAnswer(commentId: number){
    this.postService.acceptAnswer({
      postId: Number(this.postId),
      commentId: commentId
    }).pipe(takeUntil(this.destroySub$))
    .subscribe(_ => {});
  }

  ngOnDestroy(): void {
    this.destroySub$.next();
    this.destroySub$.complete();
  }
}
