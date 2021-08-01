import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import WebMap from "@arcgis/core/WebMap";
import * as projection from "@arcgis/core/geometry/projection";
import { loadModules } from 'esri-loader';
import * as MapView from 'esri/views/MapView';
import esri  = __esri 

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  public latitude = 35.892722;
  public longitude = 14.430219;
  public zoomLevel = 7;
  
@ViewChild('map',{static:true})private readonly mapElement:ElementRef;

private mapView;

  constructor() { 

    loadModules(['esri/Map', 'esri/views/MapView'])
      .then(([Map, MapView]: [esri.MapConstructor,
                              esri.MapViewConstructor]) => {
        // set default map properties
        const mapProperties = {
          basemap: 'gray'
        }
        // create map by default properties
        const map = new Map(mapProperties);
        // set default map view properties
        // container - element in html-template for locate map
        // zoom - default zoom parameter, value from 1 to 18
        const mapViewProperties = {
          container: this.mapElement.nativeElement,
          zoom: 3,
          map
        }
        // create map view by default properties
        this.mapView = new MapView(mapViewProperties);
      });
  }

  ngOnInit() {
  }

}
