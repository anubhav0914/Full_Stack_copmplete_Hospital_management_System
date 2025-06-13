import React from 'react';
import { Fade } from 'react-slideshow-image';
import 'react-slideshow-image/dist/styles.css';

const slideImages = [
  {
    url: 'https://srdalvifoundation.com/wp-content/uploads/2023/06/Blog-Image-9-1536x768.png',
    caption: 'Slide 1',
  },
  {
    url: 'https://static.toiimg.com/thumb/msid-110268129,imgsize-41358,width-400,resizemode-4/110268129.jpg',
    caption: 'Slide 2',
  },
  {
    url: 'https://static.pib.gov.in/WriteReadData/userfiles/image/image001JNY5.jpg',
    caption: 'Slide 3',
  },
];

const Slideshow = () => {
  return (
    <div className="w-full mt-32 p-8">
      <Fade>
        {slideImages.map((slideImage, index) => (
          <div key={index} className="flex justify-center items-center">
            <img
              src={slideImage.url}
              alt={slideImage.caption}
              className="max-h-[500px] w-auto object-contain rounded-xl shadow-md"
            />
          </div>
        ))}
      </Fade>
    </div>
  );
};

export { Slideshow };
