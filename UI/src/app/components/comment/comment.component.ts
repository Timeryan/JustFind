import { Component, Input, OnInit } from '@angular/core';
import { CommentService } from '../../services/comment.service';
import { Comment } from '../../models/comment.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
})
export class CommentComponent implements OnInit {
  public comments: Comment[];
  @Input() adId: string;
  textCommentAdd: string;

  constructor(
    private commentService: CommentService,
    public authService: AuthService
  ) {}
  ngOnInit(): void {
    this.commentService.comments$.subscribe((res) => (this.comments = res));
    this.onScrollDown();
  }
  public onScrollDown(): void {
    this.commentService.updateComments(this.adId);
  }
  public addComment(): void {
    this.commentService
      .createComment(this.textCommentAdd, this.adId)
      .subscribe(() => {
        this.commentService.updateComments(this.adId, true);
      });
  }
}
