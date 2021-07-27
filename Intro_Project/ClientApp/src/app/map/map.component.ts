import { Component, OnInit } from '@angular/core';
import WebMap from "@arcgis/core/WebMap";
import * as projection from "@arcgis/core/geometry/projection";

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  public latitude = 35.892722;
  public longitude = 14.430219;
  public zoomLevel = 7;
  
  constructor() { }

  ngOnInit() {
  }

}
