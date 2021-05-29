/* Copyright (c) 2006-2011 by OpenLayers Contributors (see authors.txt for 
* full list of contributors). Published under the Clear BSD license.  
* See http://svn.openlayers.org/trunk/openlayers/license.txt for the
* full text of the license. */

/**
* @requires OpenLayers/Popup/Framed.js
* @requires OpenLayers/Util.js
* @requires OpenLayers/BaseTypes/Bounds.js
* @requires OpenLayers/BaseTypes/Pixel.js
* @requires OpenLayers/BaseTypes/Size.js
*/

/**
* Class: OpenLayers.Popup.FramedCloud
* 
* Inherits from: 
*  - <OpenLayers.Popup.Framed>
*/
OpenLayers.Popup.NestFramedCloud =
  OpenLayers.Class(OpenLayers.Popup.FramedCloud, {



      /**
      * APIProperty: imageSize
      * {<OpenLayers.Size>}
      */
      imageSize: new OpenLayers.Size(1260, 729),


      /**
      * Property: positionBlocks
      * {Object} Hash of differen position blocks, keyed by relativePosition
      *     two-character code string (ie "tl", "tr", "bl", "br")
      */
      positionBlocks: {
          "tl": {
              'offset': new OpenLayers.Pixel(70, 0),
              'padding': new OpenLayers.Bounds(15, 77, 15, 15),
              'blocks': [
                { // top-left
                    size: new OpenLayers.Size('auto', 'auto'),
                    anchor: new OpenLayers.Bounds(0, 82, 22, 0),
                    position: new OpenLayers.Pixel(0, 0)
                },
                { //top-right
                    size: new OpenLayers.Size(22, 'auto'),
                    anchor: new OpenLayers.Bounds(null, 82, 0, 0),
                    position: new OpenLayers.Pixel(-1238, 0)
                },
                { //bottom-left
                    size: new OpenLayers.Size('auto', 22),
                    anchor: new OpenLayers.Bounds(0, 61, 22, null),
                    position: new OpenLayers.Pixel(0, -613)
                },
                { //bottom-right
                    size: new OpenLayers.Size(22, 22),
                    anchor: new OpenLayers.Bounds(null, 61, 0, null),
                    position: new OpenLayers.Pixel(-1238, -613)
                },
                { // stem
                    size: new OpenLayers.Size(100, 74),
                    anchor: new OpenLayers.Bounds(null, 0, 0, null),
                    position: new OpenLayers.Pixel(-7, -666)
                }
            ]
          },
          "tr": {
              'offset': new OpenLayers.Pixel(-70, 0),
              'padding': new OpenLayers.Bounds(15, 77, 15, 15),
              'blocks': [
                { // top-left
                    size: new OpenLayers.Size('auto', 'auto'),
                    anchor: new OpenLayers.Bounds(0, 82, 22, 0),
                    position: new OpenLayers.Pixel(0, 0)
                },
                { //top-right
                    size: new OpenLayers.Size(22, 'auto'),
                    anchor: new OpenLayers.Bounds(null, 82, 0, 0),
                    position: new OpenLayers.Pixel(-1238, 0)
                },
                { //bottom-left
                    size: new OpenLayers.Size('auto', 22),
                    anchor: new OpenLayers.Bounds(0, 61, 22, null),
                    position: new OpenLayers.Pixel(0, -613)
                },
                { //bottom-right
                    size: new OpenLayers.Size(22, 22),
                    anchor: new OpenLayers.Bounds(null, 61, 0, null),
                    position: new OpenLayers.Pixel(-1238, -613)
                },
                { // stem
                    size: new OpenLayers.Size(100, 73),
                    anchor: new OpenLayers.Bounds(0, 0, null, null),
                    position: new OpenLayers.Pixel(30, -667)
                }
            ]
          },
          "bl": {
              'offset': new OpenLayers.Pixel(70, 0),
              'padding': new OpenLayers.Bounds(15, 15, 15, 77),
              'blocks': [
                { // top-left
                    size: new OpenLayers.Size('auto', 'auto'),
                    anchor: new OpenLayers.Bounds(0, 21, 22, 58),
                    position: new OpenLayers.Pixel(0, 0)
                },
                { //top-right
                    size: new OpenLayers.Size(22, 'auto'),
                    anchor: new OpenLayers.Bounds(null, 21, 0, 58),
                    position: new OpenLayers.Pixel(-1238, 0)
                },
                { //bottom-left
                    size: new OpenLayers.Size('auto', 22),
                    anchor: new OpenLayers.Bounds(0, 0, 22, null),
                    position: new OpenLayers.Pixel(0, -613)
                },
                { //bottom-right
                    size: new OpenLayers.Size(22, 22),
                    anchor: new OpenLayers.Bounds(null, 0, 0, null),
                    position: new OpenLayers.Pixel(-1238, -613)
                },
                { // stem
                    size: new OpenLayers.Size(100, 72),
                    anchor: new OpenLayers.Bounds(null, null, 0, 0),
                    position: new OpenLayers.Pixel(-206, -660)
                }
            ]
          },
          "br": {
              'offset': new OpenLayers.Pixel(-70, 0),
              'padding': new OpenLayers.Bounds(15, 15, 15, 77),
              'blocks': [
                { // top-left
                    size: new OpenLayers.Size('auto', 'auto'),
                    anchor: new OpenLayers.Bounds(0, 21, 22, 58),
                    position: new OpenLayers.Pixel(0, 0)
                },
                { //top-right
                    size: new OpenLayers.Size(22, 'auto'),
                    anchor: new OpenLayers.Bounds(null, 21, 0, 58),
                    position: new OpenLayers.Pixel(-1238, 0)
                },
                { //bottom-left
                    size: new OpenLayers.Size('auto', 22),
                    anchor: new OpenLayers.Bounds(0, 0, 22, null),
                    position: new OpenLayers.Pixel(0, -613)
                },
                { //bottom-right
                    size: new OpenLayers.Size(22, 22),
                    anchor: new OpenLayers.Bounds(null, 0, 0, null),
                    position: new OpenLayers.Pixel(-1238, -613)
                },
                { // stem
                    size: new OpenLayers.Size(100, 72),
                    anchor: new OpenLayers.Bounds(0, null, null, 0),
                    position: new OpenLayers.Pixel(-169, -660)
                }
            ]
          }
      },
      /** 
      * Constructor: OpenLayers.Popup.FramedCloud
      * 
      * Parameters:
      * id - {String}
      * lonlat - {<OpenLayers.LonLat>}
      * contentSize - {<OpenLayers.Size>}
      * contentHTML - {String}
      * anchor - {Object} Object to which we'll anchor the popup. Must expose 
      *     a 'size' (<OpenLayers.Size>) and 'offset' (<OpenLayers.Pixel>) 
      *     (Note that this is generally an <OpenLayers.Icon>).
      * closeBox - {Boolean}
      * closeBoxCallback - {Function} Function to be called on closeBox click.
      */
      initialize: function (id, lonlat, contentSize, contentHTML, anchor, closeBox,
                        closeBoxCallback, option) {
          if (option) {
              OpenLayers.Util.extend(this, option);
          }
          if (!this.imageSrc) {
              this.imageSrc = OpenLayers.Util.getImagesLocation() + 'nest-popup.png';
          }
          OpenLayers.Popup.Framed.prototype.initialize.apply(this, arguments);
          this.contentDiv.className = this.contentDisplayClass;
      },

      /** 
      * APIMethod: destroy
      */
      destroy: function () {
          OpenLayers.Popup.Framed.prototype.destroy.apply(this, arguments);
      },

      CLASS_NAME: "OpenLayers.Popup.NestFramedCloud"
  });
