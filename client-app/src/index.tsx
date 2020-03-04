import React from 'react';
import ReactDOM from 'react-dom';
import {Router} from 'react-router-dom'
import 'react-toastify/dist/ReactToastify.min.css'
import 'react-widgets/dist/css/react-widgets.css'
import './app/layout/styles.css';
import 'semantic-ui-css/semantic.min.css'
import App from './app/layout/App';
import { history } from '../src/app/common/util/history'
import * as serviceWorker from './serviceWorker';
import ScrollToTop from './app/layout/ScrollToTop';
import dateFnsLocalizer from 'react-widgets-date-fns'
import { MatomoProvider, createInstance } from '@datapunt/matomo-tracker-react'

dateFnsLocalizer();

const instance = createInstance({
  urlBase: "http://localhost/Matomo",
  siteId: 1, 
  trackerUrl: "http://localhost/Matomo/matomo.php", 
  srcUrl: "http://localhost/Matomo/matomo.js" 
});

ReactDOM.render(
    <Router history={history}>
    <ScrollToTop>
          <MatomoProvider value={instance}>
            <App />
          </MatomoProvider>    
    </ScrollToTop>
    </Router>,
  document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
