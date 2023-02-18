'use strict';
import express from 'express';
import path from 'path';
import cors from 'cors';
import bodyParser from 'body-parser';

import compression from 'compression';
import dotenv from 'dotenv';
import cookieParser from 'cookie-parser';
dotenv.config();

import user from './user.js';

const pageDir = '../../pages/login';
const app = express();

app.use(cors());
app.use(cookieParser());
app.use(bodyParser.json());
app.use(compression());
app.use(express.static(path.join(pageDir)));
app.use('/user', user);

const port = process.env.PORT || 3000;

app.listen(port, () => {
    console.log(`Listening on port ${port}`);
});


