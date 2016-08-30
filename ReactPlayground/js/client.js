import React from "react";
import ReactDom from "react-dom";
//import Bootstrap from "./vendor/bootstrap-without-jquery"
import {Router, Route, IndexRoute, hashHistory} from "react-router";

import Archives from "./pages/Archives";
import Featured from "./pages/Featured";
import Layout from "./pages/Layout";
import Settings from "./pages/Settings";

const app = document.getElementById('app');

ReactDom.render(
    <Router history={hashHistory}>
        <Route path="/" component={Layout}>
            <IndexRoute component={Featured}></IndexRoute>
            <Route path="archives(/:article)" component={Archives}></Route>
            <Route path="settings" component={Settings}></Route>
        </Route>
    </Router>,
    app);