import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';


@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  public latitude = 35.892722;
  public longitude = 14.430219;
  public zoomLevel = 7;
  
private mapView;

  constructor() { 

  }

  ngOnInit() {
  }

}
