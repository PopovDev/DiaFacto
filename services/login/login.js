import express from 'express';
import verify from './google-verify.js';

const router = express.Router();

router.post('/', async (req, res) => {
    const { credential } = req.body;
    const payload = await verify(credential);
    if (payload) {
        res.send('ok');
    } else {
        res.send('error');
    }
    console.log(payload);
});

export default router;