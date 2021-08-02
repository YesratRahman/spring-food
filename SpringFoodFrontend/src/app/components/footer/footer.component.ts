import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  onClickedGit()
  {
    window.open("https://github.com/YesratRahman");
  
  }
  onClickLinked()
  {
    window.open("https://www.linkedin.com/in/yesrat-rahman-84743717a/");
  
  }
}
