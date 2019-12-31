import React, {useCallback} from 'react'
import {useDropzone} from 'react-dropzone'
import { Icon, Header } from 'semantic-ui-react'

interface IProps {
    setFiles: (files: Object[]) => void;
}

const dropzoneStyles = {
    border: 'dashed 3px',
    borderColor: '#eee',
    borderRadius: '5px',
    paddingTop: '30px',
    textAlign: 'center' as 'center', //this wierd syntax is required to get over an error that center doesn't counted as string
    height: '200px'
}

const dropzoneActive = {
    borderColor: 'green'
}

const PhotoWidgetDropzone: React.FC<IProps> = ({setFiles}) => {
  const onDrop = useCallback(acceptedFiles => {
    setFiles(acceptedFiles.map((file: Object) => Object.assign(file,
        { preview: URL.createObjectURL(file)
        }))) 
  }, [setFiles])
  const {getRootProps, getInputProps, isDragActive} = useDropzone({onDrop})

  return (
    <div {...getRootProps()} 
      style={ isDragActive ? { ...dropzoneStyles, ...dropzoneActive}
        : dropzoneStyles
    }>
      <input {...getInputProps()} />
      <Icon name='upload' size='huge' />
      <Header content='Drop image here' />
    </div>
  )
}

export default PhotoWidgetDropzone;