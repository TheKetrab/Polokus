import $ from 'jquery';
import BpmnModeler from 'bpmn-js/lib/Modeler';
import BpmnViewer from 'bpmn-js/lib/NavigatedViewer';

import {
  BpmnPropertiesPanelModule,
  BpmnPropertiesProviderModule
} from 'bpmn-js-properties-panel';
import PolokusPropertiesProviderModule from './provider/polokus';

import diagramXML from '../resources/newDiagram.bpmn';

import '../styles/app.less';

var container = $('#js-drop-zone');
var mode; // viewer or to edit
var bpmnModeler;
var bpmnViewer;




async function openInViewer(bpmnXML) {

  // import diagram
  try {

    await
     bpmnViewer.importXML(bpmnXML);
debugger;
    // access viewer components
    var canvas = bpmnViewer.get('canvas');
    var overlays = bpmnViewer.get('overlays');


    // zoom to fit full viewport
    canvas.zoom('fit-viewport');

    // // attach an overlay to a node
    // overlays.add('SCAN_OK', 'note', {
    //   position: {
    //     bottom: 0,
    //     right: 0
    //   },
    //   html: '<div class="diagram-note">Mixed up the labels?</div>'
    // });

    // add marker
    //canvas.addMarker('SCAN_OK', 'needs-discussion');
  } catch (err) {

    console.error('could not import BPMN 2.0 diagram', err);
  }
}





async function createNewDiagram() {
  await openDiagram(diagramXML);
  return;
  setInactiveNodes(['StartEvent_1']);
}

async function openDiagram(xml) {

  try {

    await openInViewer(xml);
    //await bpmnModeler.importXML(xml);
    //await bpmnViewer.importXML(xml);

    container
      .removeClass('with-error')
      .addClass('with-diagram');
  } catch (err) {

    container
      .removeClass('with-diagram')
      .addClass('with-error');

    container.find('.error pre').text(err.message);

    console.error(err);
  }
}

function download(encodedData,filename) {
  var element = document.createElement('a');
  element.setAttribute('href', 'data:text/plain;charset=utf-8, ' + encodedData);
  element.setAttribute('download', filename);
  document.body.appendChild(element);
  element.click();
  document.body.removeChild(element);
}

async function readFileAsync(file) {
  return new Promise((resolve, reject) => {
    let reader = new FileReader();

    reader.onload = () => {
      resolve(reader.result);
    };

    reader.onerror = reject;

    reader.readAsText(file,'UTF-8');
  })
}

// ----- ----- ----- ----- -----
//       ----- PUBLIC -----
// ----- ----- ----- ----- -----
window.api = {}

async function xml2Svg(xml) {

  let modeler = bpmnModeler; //new BpmnModeler();
  await modeler.importXML(xml);
  let { svg } = await modeler.saveSvg();

  return svg;
}
window.api.xml2Svg = xml2Svg;

async function downloadSvgFile() {

  const { svg } = await bpmnModeler.saveSVG();

  let encodedData = encodeURIComponent(svg);
  download(encodedData,"diagram.svg");

}
window.api.downloadSvgFile = downloadSvgFile;

async function downloadBpmnFile() {

  const { xml } = await bpmnModeler.saveXML({ format: true });

  let encodedData = encodeURIComponent(xml);
  download(encodedData,"diagram.bpmn");

}
window.api.downloadBpmnFile = downloadBpmnFile;

function setActiveNodes(nodesIds) {

  var modeling = bpmnModeler.get('modeling');
  var elementRegistry = bpmnModeler.get('elementRegistry');

  var elements = nodesIds.map(x => elementRegistry.get(x));

  modeling.setColor(elements, {
    stroke: 'green',
    fill: 'green'
  });

}
window.api.setActiveNodes = setActiveNodes;

function setInactiveNodes(nodesIds) {

  var modeling = bpmnModeler.get('modeling');
  var elementRegistry = bpmnModeler.get('elementRegistry');

  var elements = nodesIds.map(x => elementRegistry.get(x));

  modeling.setColor(elements, {
    stroke: 'black',
    fill: 'white'
  });

}
window.api.setInactiveNodes = setInactiveNodes;


async function loadBpmn(xml) {

  await bpmnModeler.importXML(xml);
}
window.api.loadBpmn = loadBpmn;


async function getSvg() {
  const {svg} = await bpmnModeler.getSvg();
  return svg;
}
window.api.getSvg = getSvg;



function initialize(mode) {

  if (mode == 'modeler') {
  
    // CREATE MODELER
    bpmnModeler = new BpmnModeler({
      container: '#js-canvas',
      propertiesPanel: {
        parent: '#js-properties-panel'
      },
      additionalModules: [
        BpmnPropertiesPanelModule,
        BpmnPropertiesProviderModule,
        PolokusPropertiesProviderModule
      ],
      moddleExtensions: {
    
      }
    });

    $('#js-download-diagram').on('click', function(e) {
      downloadBpmnFile();
    });
    $('#js-download-svg').on('click', function(e) {
      downloadSvgFile();
    });
    $('#js-download-png').on('click', function(e) {
      var svg = window.api.getSvg();
      if (callbackObj) {
        callbackObj.downloadPng(svg);
      }    
    });
    $('#js-open-file').on('change', async function() {
  
      var file = this.files[0];
      var text = await readFileAsync(file);
      await window.api.loadBpmn(text);
    });
  

    createNewDiagram();

  }
  
  else {

    // CREATE VIEWER
    bpmnViewer = new BpmnViewer({ 
      container: '#js-canvas'
    })

    $('.buttons-panel').css('display','none');
  
  }

}

// ----- ----- ----- ----- -----
//       ----- MAIN -----
// ----- ----- ----- ----- -----
$(function() {

  const params = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
  });
  
  if (params.mode) {
    mode = params.mode;
  } else {
    mode = "modeler"
  }

  initialize(mode);



});
// ----- ----- ----- ----- -----



