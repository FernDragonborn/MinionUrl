import { MinionUrlService } from './service/minion-url.service';
import { Component, OnInit } from '@angular/core';
import { Url } from 'src/models/url.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'minionUrl';
  urls: Url[] = [];
  url: Url = { 
    fullUrl: '' 
  }; 

  constructor(private minionUrlService: MinionUrlService) {}

  ngOnInit(): void {
    this.getAllUrls();
  }

  getAllUrls() {
    this.minionUrlService.getAllUrls().subscribe((response) => {
      this.urls = response;
      console.log(response);
    });
  }

  onSubmit() {
    this.minionUrlService.addUrl(this.url).subscribe((response) => {
      this.getAllUrls();
      this.url = {
        fullUrl: '',
        creatorId: '0',
        creationDateTime: '',
      };
      console.log(response);
    });
  }
}
