import React, { Fragment, useState, useEffect } from 'react';
import { Header, Grid, Button } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import PhotoWidgetDropzone from './PhotoWidgetDropZone';
import PhotoWidgetCropper from './PhotoWidgetCropper';

interface IProps {
  loading: boolean;
  uploadPhoto: (file: Blob) => void;
}

export const PhotoUploadWidget: React.FC<IProps> = ({loading, uploadPhoto}) => {
    const [files, setFiles] = useState<any[]>([]);//we're setting this as any array to bypass type checking with our dropzone widget
    const [image, setImage] = useState<Blob | null>(null);

    useEffect(() => { //we must run this effect (similar to component did unmount) to clear the memory of the client from all the photo preview
                      //since if the user plays with photos, he may have a lot of them stuck in memory and cause memory leak
        return () => {
            files.forEach(file => URL.revokeObjectURL(file.preview))
        }
    })

    return (
        <Fragment>
          <Grid>
            <Grid.Column width={4}>
              <Header color='teal' sub content='Step 1 - Add Photo' />
              <PhotoWidgetDropzone setFiles={setFiles} />
            </Grid.Column>
            <Grid.Column width={1} />
            <Grid.Column width={4}>
              <Header sub color='teal' content='Step 2 - Resize image' />
              {files.length > 0 &&
              <PhotoWidgetCropper setImage={setImage} imagePreview={files[0].preview} />
              }
            </Grid.Column>
            <Grid.Column width={1} />
            <Grid.Column width={4}>
              <Header sub color='teal' content='Step 3 - Preview & Upload' />
              {files.length > 0 &&
              <Fragment>
                  <div className='img-preview' style={{minHeight: '200px', overflow: 'hidden'}} />
                  <Button.Group widths={2}>
                    <Button positive icon='check' loading={loading} onClick={() => uploadPhoto(image!)} />
                    <Button icon='close' disabled={loading} onClick={() => setFiles([])} />
                  </Button.Group>              
              </Fragment>
              
              }
            </Grid.Column>
          </Grid>
        </Fragment>
      );      
}

export default observer(PhotoUploadWidget);
