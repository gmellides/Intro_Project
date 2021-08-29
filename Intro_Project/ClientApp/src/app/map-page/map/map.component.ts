import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { loadModules } from 'esri-loader';
import esri = __esri;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {  
  @Input() public latitude:number;
  @Input() public longitude:number;
  @Input() public zoomLevel:number; 
  @Input() public isMiniMap:boolean;

  private _view: esri.MapView = null;

  @ViewChild("mapView", { static:true } ) private map:ElementRef;

  constructor() {  }

  ngOnInit() {   
    return loadModules([
      'esri/Map',
      'esri/views/MapView'
    ])
      .then(([Map, MapView]) => {
        const map: __esri.Map = new Map({
          basemap: 'hybrid'
        });

        this._view = new MapView({
          container: this.map.nativeElement,
          center: [this.latitude , this.longitude],
          zoom: this.zoomLevel,
          map: map
        });

        // Add map click event when component runs in /map page. 
        if(!this.isMiniMap){

        }

        this._view.center.latitude = this.latitude;
        this._view.center.longitude = this.longitude;
        this._view.zoom = this.zoomLevel;
      })
      .catch(err => {
        console.error(err);
      });
  }

}
